using e_Estoque_API.Core.Entities;

namespace e_Estoque_API.Core.Fakes
{
    public class GenerateCategoryFake
    {
        public IEnumerable<Category> CreateValid(int qtd)
        {
            var lorem = new Bogus.DataSets.Lorem("pt-Br");
            var list = new List<Category>();

            for (int i = 0; i < qtd; i++)
            {
                var category = new Category(
                        Guid.NewGuid(),
                        lorem.Word(),
                        lorem.Sentence(1, 5),
                        lorem.Sentence(1, 2)
                    );

                list.Add(category);
            }

           return list;
        }

        public IEnumerable<Category> CreateInValid(int qtd)
        {
            var list = new List<Category>();

            for (int i = 0; i < qtd; i++)
            {
                var category = new Category(
                        Guid.Empty,
                        "",
                        "",
                        ""
                    );

                list.Add(category);
            }

            return list;
        }
    }
}
