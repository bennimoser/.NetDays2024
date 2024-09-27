namespace Resharper.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // new HttpClient().var

            // httpclient.timeout == 15.if

            // httpclient.timeout == 15.not/switch/var/...

            // httpclient.null

            // httpclient.notnull

            // Extract Method.
            var convertedStrings = new List<string>();
            var list = new List<string>();
            foreach (var stringlist in list)
            {
                var convertedString = stringlist;
                convertedString += "converted";
                convertedString = convertedString.Split(" ").First();
                convertedStrings.Add(convertedString);
            }
        }
    }
}
