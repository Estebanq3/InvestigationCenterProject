
namespace ResearchRepository.Domain.Core.Entities
{
    public interface ISoftDeletable
    {
        bool Deleted { get; set; }
    }
}
