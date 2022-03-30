namespace Counters
{
    public class Counter
    {
        public string Name { get; set; }
        public Counter(string Name)
        {
            this.Name = Name;
        }
        public static string[] UniqWordCounter(Counter[] counter)
        {
            string words = "";
            int count = 0;

            for (int i = 0; i < counter.Length; i++)
            {
                if (!words.Contains(counter[i].Name))
                {
                    words += counter[i].Name + " ";
                    count++;
                }
            }
            words = words.Remove(words.Length - 1);
            var word = words.Split(new char[] { ' ' }, count);
            return word;
        }
    }
}