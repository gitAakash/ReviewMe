namespace ReviewMe.Model
{
    public interface ISoftDelete
    {
        bool IsActive { get; set; }
    }
}