using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Module18
{
    internal abstract class Command
    {
        protected YoutubeLoader _youtube;
        public readonly string Url;                 // может понадобиться для лога

        public Command(YoutubeLoader youtube, string Url)
        {
            _youtube = youtube;
            _youtube.Url = Url;
            this.Url = Url;
        }

        public abstract Task Execute();
        public virtual void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
