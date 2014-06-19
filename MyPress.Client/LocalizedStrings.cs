using MyPress.Client.Resources;

namespace MyPress.Client
{
    public class LocalizedStrings
    {
        public LocalizedStrings()
        {
        }

        private static Resource1 resource1 = new Resource1();

        public Resource1 Resource1 { get { return resource1; } } 
    }
}