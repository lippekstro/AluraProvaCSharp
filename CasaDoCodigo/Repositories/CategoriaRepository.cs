using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        void AddCat(string nome);
        Categoria GetCategoria(int categoriaId);
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public void AddCat(string nome) //adiciona categorias
        {
            if (!dbSet.Where(c => c.Nome == nome).Any())
            {
                dbSet.Add(new Categoria(nome));
                contexto.SaveChanges();
            }
        }

        public Categoria GetCategoria(int categoriaId) //retorna categorias
        {
            return dbSet.Where(c => c.Id == categoriaId).FirstOrDefault();
        }
    }

    
}
