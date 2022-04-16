using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Module18
{
    internal class LoadFileCommand : Command
    {
        public LoadFileCommand(YoutubeLoader youtube, string Url) : base(youtube, Url) { }

        public override async Task Execute() => await _youtube.LoadFileAsync();
    }
}
