using System;

namespace PlurCrawler.Attributes
{
    /// <summary>
    /// 지정된 노트를 저장하는 특성입니다. 이 클래스는 상속될 수 없습니다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public sealed class NoteAttribute : Attribute
    {
        /// <summary>
        /// <see cref="NoteAttribute"/> 클래스를 초기화합니다.
        /// </summary>
        /// <param name="message"></param>
        public NoteAttribute(string message)
        {
            this.Message = message;
        }
        
        public string Message { get; set; }
    }
}
