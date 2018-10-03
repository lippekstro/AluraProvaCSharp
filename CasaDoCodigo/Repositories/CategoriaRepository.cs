using System.Collections.Generic;
using System.Linq;
using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        void AddCat(Categoria categoria);
        Categoria GetCategoria(int categoriaId);
        Categoria GetCategoria(string categoria);
        List<Categoria> GetCategorias();
        void AddListaCat(IList<string> lista);
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public void AddCat(Categoria categoria) //adiciona categorias
        {
            if (!dbSet.Where(c => c.Nome == categoria.Nome).Any())
            {
                dbSet.Add(new Categoria(categoria.Nome));
                contexto.SaveChanges();
            }
        }

        public void AddListaCat(IList<string> lista) //adiciona categorias em lista
        {
            foreach (var categoria in lista)
            {
                if(this.GetCategoria(categoria) == null)
                {
                    AddCat(new Categoria(categoria));
                }
            }
        }

        public Categoria GetCategoria(int categoriaId) //retorna categorias pelo id
        {
            return dbSet.Where(c => c.Id == categoriaId).FirstOrDefault();
        }

        public Categoria GetCategoria(string categoria) //retorna categorias pelo nome
        {
            return dbSet.Where(c => c.Nome == categoria).FirstOrDefault();
        }

        public List<Categoria> GetCategorias()
        {
            return dbSet.ToList();
        }
    }

    
}
