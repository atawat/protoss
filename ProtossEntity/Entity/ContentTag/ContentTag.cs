using Protoss.Entity.Model;
using YooPoon.Core.Data;

namespace Protoss.Entity.Model
{
    public class ContentTag:IBaseEntity
    {
        public int Id { get; set; }

        public virtual ContentEntity Content { get; set; }

        public virtual TagEntity Tag { get; set; }
    }
}