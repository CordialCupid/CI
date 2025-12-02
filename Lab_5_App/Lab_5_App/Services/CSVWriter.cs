namespace Lab_5_App.Services
{
    public class CSVWriter : ICSVWriter
    {
        /// <summary>
        /// Method to allow appending of generic type to end of CSV file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Obj"></param>
        /// <param name="path"></param>
        public void WriteToCSV<T>(T Obj, string path)
        {
            var props = typeof(T).GetProperties();
            var values = props.Select(p => p.GetValue(Obj));
            var line = string.Join(",", values);

            File.AppendAllText(path, line + Environment.NewLine);
        }

        /// <summary> 
        /// Method to allow repopulating of CSV file given a list of generic objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList"></param>
        /// <param name="path"></param>
        public void OverwriteCSV<T>(List<T> objList, string path)
        {
            File.WriteAllText(path, "");

            foreach (T obj in objList)
            {
                WriteToCSV(obj, path);
            }       
        }
    }
}
