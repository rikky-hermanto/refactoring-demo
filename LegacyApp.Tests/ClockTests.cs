namespace LegacyApp.Tests
{
    public class ClockTests
    {
        [Fact]
        public void Now_Returns_Fixed_DateTime()
        {
            // Arrange
            DateTime fixedDate = new DateTime(2022, 5, 15);
            Clock clock = new Clock(fixedDate);

            // Act
            DateTime actual = clock.Now;

            // Assert
            Assert.Equal(fixedDate, actual);
        }
    }
}
