
namespace Gwearable
{    
    public class Tokens
    {
        public string[] elements;
        public int position = 0;
        public int maxindex = 0;
        public Tokens(string source, char[] delimiters)
        {
            elements = source.Split(delimiters);
            maxindex = elements.GetLength(0);
        }
        public string GetFirstNotEmptyString()
        {
            for (int i = 0; i < maxindex; i++)
            {
                if (elements[i]!=string.Empty)
                {
                    position = i;
                    return elements[i];
                }
                else
                {
                    continue;
                }
            }
            return "";
        }
        
    }
}