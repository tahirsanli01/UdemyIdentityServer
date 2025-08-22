namespace ADASOIdentityServer.AuthServer.UI.Models.ComponentViewDtos
{
    public class PagingDto
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<ColumnDto> columns { get; set; }
        public List<OrderDto> order { get; set; }
        public SearchDto search { get; set; }
    }

    public class ColumnDto
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public SearchDto search { get; set; }
    }

    public class OrderDto
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class SearchDto
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }

}
