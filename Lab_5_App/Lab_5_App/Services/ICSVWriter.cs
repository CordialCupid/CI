namespace Lab_5_App.Services
{
    public interface ICSVWriter
    {
        public void WriteToCSV<T>(T obj, string path);

        public void OverwriteCSV<T>(List<T> objList, string path);
    }
}
