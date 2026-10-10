

using System.Text.RegularExpressions;

namespace PersonalFinanceMng.Domain.Entities
{
  
    public class PaymentMethod
    {
        public int Id { get; set; }
      
        public string Name { get; set; } = string.Empty;
        public string NormalizedName { get; set; } = string.Empty;
 
        public string? Description { get; set; }
       
        public bool IsActive { get; set; } = true;
    
        public int? UserId { get; set; }


        private PaymentMethod() { }
        public PaymentMethod(string name, string? description, int? userId, bool isActive = true)
        {
            //if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("O Nome do método deve ser fornecido: ", nameof(name));
            SetName(name);
            Description = description;
            UserId = userId;
            IsActive = isActive;
        }
      
        public PaymentMethod(int id, string name, string? description, int? userId, bool isActive = true)
        : this(name, description, userId, isActive)
        {
            Id = id;
        }


        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException("O Nome do método deve ser fornecido: ", nameof(name)); }
            
            Name = name.Trim();


            NormalizedName = Regex.Replace(Name.ToLowerInvariant(), @"\s+", " ");
        }

        public void UpdateDetails(string? name, string description)
        {
            if (name != null)
            {
                SetName(name);
            }
           
            Description = description;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public override bool Equals(object? obj)
        {
            // 1. Se for nulo ou de tipo diferente, não é igual
            if (obj is not PaymentMethod other)
                return false;

            // 2. Se for a mesma referência na memória, é igual
            if (ReferenceEquals(this, other))
                return true;

            // 3. Se for uma entidade temporária (ainda não salva no banco), 
            // não podemos comparar por Id=0
            if (Id == 0 || other.Id == 0)
                return false;

            // 4. Se ambas já possuem Id do banco, compara pelos Ids
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            // Se a entidade ainda é nova (Id == 0), usa a referência base da memória.
            // Se já tem Id, usa o Id para o hash.
            if (Id == 0)
                return base.GetHashCode();

            return HashCode.Combine(GetType(), Id);
        }
    }
}
