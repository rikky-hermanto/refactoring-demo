using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrucutreAlgo
{
    //https://leetcode.com/company/atlassian/discuss/5605423/Atlassian-P60-Phone-Screen-or-Cleared-and-moved-to-Onsite
    // Imagine a system that stores files, which can be grouped into collections. Our goal is to determine where our resources are being used most.
    //     For this system, we would like to generate a report that includes:
    //
    // The total size of all files stored.
    //     The top N collections (by total file size), where N is a user-defined value.

    // An example input for the report generator might look like this:
    // file1.txt (size: 100)
    // file2.txt (size: 200) in collection "collection1"
    // file3.txt (size: 200) in collection "collection1"
    // file4.txt (size: 300) in collection "collection2"
    // file5.txt (size: 10)
    // In this context, collections function like tags, and files can be tagged accordingly. We need to implement methods that return:
    //
    // The total size of all files in the system.
    //     The top N collections, based on the total size of the files tagged with each collection.
    //
    //     For the example above,
    //      "collection1" contains two files with a total size of 400,
    //      "collection2" contains one file with a total size of 300,
    //      and there are two files without any collection, totaling 110.
    //
    //     If the user requests the top 2 collections, the result would be: ["collection1", "collection2"].

    //  Approach: Hashmap + Priority Queue. File: size, tags (zero - multiple tags)
    public class TopNFileTests
    {
        // ✅ **1. Basic Test Case**
        [Fact]
        public void TestTotalSizeAndTopNCollections()
        {
            var tracker = new FileTracker();
            tracker.AddFile("file1.txt", 100);
            tracker.AddFile("file2.txt", 200, new List<string> { "collection1" });
            tracker.AddFile("file3.txt", 200, new List<string> { "collection1" });
            tracker.AddFile("file4.txt", 300, new List<string> { "collection2" });

            Assert.Equal(800, tracker.GetTotalSize());
            Assert.Equal(new List<string> { "collection1", "collection2" }, tracker.GetTopNCollections(2));
        }

        // ✅ **2. Files Without Collections**
        [Fact]
        public void TestFilesWithoutCollections()
        {
            var tracker = new FileTracker();
            tracker.AddFile("file1.txt", 150);
            tracker.AddFile("file2.txt", 250);

            Assert.Equal(400, tracker.GetTotalSize());
            Assert.Empty(tracker.GetTopNCollections(1)); // No collections exist
        }

        // ✅ **3. Single File in Multiple Collections**
        [Fact]
        public void TestFileInMultipleCollections()
        {
            var tracker = new FileTracker();
            tracker.AddFile("file1.txt", 500, new List<string> { "A", "B" });

            Assert.Equal(500, tracker.GetTotalSize());
            Assert.Equal(new List<string> { "A", "B" }, tracker.GetTopNCollections(2));
        }

        // ✅ **4. No Files in System**
        [Fact]
        public void TestNoFiles()
        {
            var tracker = new FileTracker();
            Assert.Equal(0, tracker.GetTotalSize());
            Assert.Empty(tracker.GetTopNCollections(3));
        }

        // ✅ **5. All Files Without Collections**
        [Fact]
        public void TestOnlyUncategorizedFiles()
        {
            var tracker = new FileTracker();
            tracker.AddFile("file1.txt", 300);
            tracker.AddFile("file2.txt", 700);

            Assert.Equal(1000, tracker.GetTotalSize());
            Assert.Empty(tracker.GetTopNCollections(2));
        }

        // ✅ **6. Top N Larger Than Available Collections**
        [Fact]
        public void TestTopNLargerThanAvailableCollections()
        {
            var tracker = new FileTracker();
            tracker.AddFile("file1.txt", 500, new List<string> { "collection1" });

            Assert.Equal(new List<string> { "collection1" }, tracker.GetTopNCollections(5));
        }

        // ✅ **7. Files With Zero Size**
        [Fact]
        public void TestFilesWithZeroSize()
        {
            var tracker = new FileTracker();
            tracker.AddFile("file1.txt", 0, new List<string> { "A" });
            tracker.AddFile("file2.txt", 100, new List<string> { "B" });

            Assert.Equal(100, tracker.GetTotalSize());
            Assert.Equal(new List<string> { "B" }, tracker.GetTopNCollections(2));
        }

        // ✅ **8. Multiple Collections with Same Total Size**
        [Fact]
        public void TestTiedCollections()
        {
            var tracker = new FileTracker();
            tracker.AddFile("file1.txt", 300, new List<string> { "A" });
            tracker.AddFile("file2.txt", 300, new List<string> { "B" });

            Assert.Equal(new List<string> { "A", "B" }, tracker.GetTopNCollections(2));
        }

        // ✅ **9. Duplicate File Names with Different Sizes**
        [Fact]
        public void TestDuplicateFileNames()
        {
            var tracker = new FileTracker();
            tracker.AddFile("file1.txt", 100, new List<string> { "A" });
            tracker.AddFile("file1.txt", 200, new List<string> { "A" });

            Assert.Equal(300, tracker.GetTotalSize());
            Assert.Equal(new List<string> { "A" }, tracker.GetTopNCollections(1));
        }

        // ✅ **10. Negative or Invalid File Sizes**
        [Fact]
        public void TestNegativeFileSize()
        {
            var tracker = new FileTracker();
            Assert.Throws<ArgumentException>(() => tracker.AddFile("file1.txt", -100, new List<string> { "A" }));
        }
    }

    public class File
    {
        public string Filename { get; set; }
        public int Size { get; set; }
        public HashSet<string> Tags { get; set; }
    }

    public class FileTracker
    {
        private readonly Dictionary<string, (List<File> files, int subTotalSize)> _collections = new Dictionary<string, (List<File> files, int subTotalSize)>();
        private int _totalSize = 0;
        private PriorityQueue<string, int> _heap = new PriorityQueue<string, int>();

        public void AddFile(string filename, int size, List<string>? tags = null) {
            if (size < 0)
                throw new ArgumentException("invalid file size.");

            tags ??= new List<string>();

            var file = new File {
                Filename = filename,
                Size = size,
                Tags = tags.ToHashSet()
            };

            _totalSize += size;

            //Map collections
            foreach (var tag in tags) {
                if (!_collections.TryGetValue(tag, out (List<File> files, int subTotalSize) collection)) {
                    collection = (new List<File>(), 0);
                    _collections[tag] = collection;
                }

                var files = collection.files;
                var subTotalSize = collection.subTotalSize;
                collection.files.Add(file);

                _collections[tag] = (files, subTotalSize + size);
            }

            //Map to heap
            var desc = new DescendingComparer();
            _heap = new PriorityQueue<string, int>(comparer:desc);

            foreach (var collection in _collections) {
                _heap.Enqueue(collection.Key, collection.Value.subTotalSize );
            }
        }

        public int GetTotalSize() {
            return _totalSize;
        }

        private class DescendingComparer : IComparer<int>
        {
            public int Compare(int x, int y) => y.CompareTo(x); //descending
        }

        public IEnumerable<string> GetTopNCollections(int n) {
            var topNCollections = new List<string>();

            var heapCount = _heap.Count;
            for (var i = 0; i < heapCount && i < n; i++) {
                var tag =_heap.Dequeue();

                if(_collections[tag].subTotalSize > 0)
                    topNCollections.Add(tag);
            }

            return topNCollections;
        }
    }
}
