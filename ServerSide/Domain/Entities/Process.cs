
namespace MiniMesTrainApi.Domain.Entities;

public class Process
{
    public int Id { get; set; }
    public string SerialNumber { get; set; } = "";
    public long OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int StatusId { get; set; } // check it

    public ProcessStatus ProcessStatus { get; set; } = null!;
    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public ICollection<ProcessParameter> ProcessParameters { get; set; } = null!;
}