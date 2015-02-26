using System;

namespace ReviewMe.Common.Extensions
{
    [Serializable]
    public class SerializableSelectListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }
}