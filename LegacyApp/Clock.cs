namespace LegacyApp
{
    public class Clock : IClock
    {
        private readonly DateTime _fixedDate;

        public Clock(DateTime fixedDate) {
            _fixedDate = fixedDate; 
        }
        public DateTime Now => _fixedDate;
    }
}