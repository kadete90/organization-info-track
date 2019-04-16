namespace InfoTrack.App.Models
{
    public class GenericResult<T> where T : class
    {
        public T Result { get; set; }

        public bool Successful { get; }

        public string Error { get; set; }
        

        public GenericResult(string error)
        {
            Successful = false;
            Error = error;
        }

        public GenericResult(T result)
        {
            Successful = true;
            Result = result;
        }
    }
}
