using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrucutreAlgo
{
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
}
