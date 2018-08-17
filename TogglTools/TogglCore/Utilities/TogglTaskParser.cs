using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.TogglCore
{
    public static class TogglTaskParser
    {
        public static string GetStoryName(string description)
        {
            Argument.IsNotNull(description);

            var splitDescription = description.Split(']');

            var story = "";

            if (splitDescription.Length == 1)
            {
                story = description.Trim();
            }
            else if (splitDescription.Length == 2)
            {
                story = $"{splitDescription[0].Trim()}] {splitDescription[1].Trim()}";
            }
            else if (splitDescription.Length == 3)
            {
                story = $"{splitDescription[0].Trim()}] {splitDescription[2].Trim()}";
            }

            return story;
        }
    }
}
