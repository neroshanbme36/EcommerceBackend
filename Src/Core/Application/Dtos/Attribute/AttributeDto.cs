namespace Application.Dtos.Attribute
{
    public class AttributeDto
    {
        public string Name {get; set;} = string.Empty;
        public string? PluralName {get; set;} 
        public string? PrefixValueText {get; set;}
        public string? SuffixValueText {get; set;}
        public string FilterType {get; set;} = string.Empty; // Include, Exclude, Range
        public string Style {get; set;} = string.Empty; //Checkbox,MultiCheckbox,Slider,Rating
        public int Priority {get; set;}
        public bool IsSearchBar {get; set;}
        public bool IsSearchable {get; set;}
        public IReadOnlyList<AttributeValueDto> AttributeValues {get; set;} = new List<AttributeValueDto>();
        public IReadOnlyList<AttributeValueRangeDto> AttributeValueRangeDtos {get; set;} = new List<AttributeValueRangeDto>();
    }
}