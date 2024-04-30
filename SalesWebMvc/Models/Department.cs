namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        // total de vendas do departamento por data 
        public double TotalSales(DateTime initial, DateTime final)
        {
            //calcular o total de vendas do departamento para o entervalo de datas
            //pegar lista de vendedores
            // somar o total de vendas de cada vendedor 
            // no intervalo de data passado 
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
