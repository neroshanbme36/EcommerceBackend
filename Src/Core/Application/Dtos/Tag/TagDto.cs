namespace Application.Dtos.Tag
{
    public class TagDto
    {
        public long Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;}
    }
}