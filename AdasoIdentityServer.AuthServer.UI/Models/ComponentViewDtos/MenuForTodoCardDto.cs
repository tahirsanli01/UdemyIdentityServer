namespace AdasoAdvisor.Models.ComponentViewDtos
{
    public class MenuForTodoCardDto
    {
        public MenuForTodoCardDto(TodoCardType cardType)
        {
            CardType = cardType;
        }

        public TodoCardType CardType { get; set; }

    }

    public enum TodoCardType
    {
        Bekliyor = 1,
        DevamEdiyor = 2,
        Tamamlandi = 3
    }
}
