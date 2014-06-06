using MyPress2.Resources;

namespace MyPress2
{
    public class LocalizedStrings
    {
        public LocalizedStrings()
        {
        }

        private static MyPress2.Resources.Resource1 resource1 = new Resource1();

        public MyPress2.Resources.Resource1 Resource1 { get { return resource1; } } 
    }
}