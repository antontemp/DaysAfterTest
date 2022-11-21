namespace Script.Core
{
    public class GeneratorId
    {
        private int _id = 0;

        public int NextId()
        {
            return ++_id;
        }

    }
}