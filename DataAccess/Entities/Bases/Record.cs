#nullable disable

namespace DataAccess.Entities.Bases
{
    public abstract class Record
    {
        public int Id { get; set; }

        public string Guid { get; set; }
    }
}
