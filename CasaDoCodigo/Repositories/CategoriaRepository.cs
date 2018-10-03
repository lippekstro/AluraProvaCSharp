using System.Collections.Generic;
using System.Linq;
using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Categoria GetCategoria(int categoriaId);
        Categoria GetCategoria(string categoria);
        List<Categoria> GetCategorias();
        void AddCat(string categoria);
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public void AddCat(string categoria) //adiciona categoria
        {
            var categoriaNome = dbSet.Where(c => c.Nome == categoria).FirstOrDefault();
            if (categoriaNome == null)
            {
                categoriaNome = new Categoria(categoria);
                contexto.Add(categoriaNome);
            }
            contexto.SaveChanges();
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
