using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository categoriaRepository;

        public ProdutoRepository(ApplicationContext contexto,
            ICategoriaRepository categoriaRepository) : base(contexto)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public IList<Produto> GetProdutos()
        {
            return dbSet.Include(p => p.Categoria).ToList();
        }

        public List<Produto> GetProdutos(int id)
        {
            return dbSet.Where(p => p.Categoria.Id == id).Include(p => p.Categoria).ToList();
        }

        public void SaveProdutos(List<Livro> livros)
        {
            var categorias = new List<string>();

            foreach (var livro in livros)
            {
                categorias.Add(livro.Categoria);
            }
            categoriaRepository.AddListaCat(categorias);

            foreach (var livro in livros)
            {
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    var categoria = categoriaRepository.GetCategoria(livro.Categoria);
                    if (categoria == null)
                    {
                        categoria = new Categoria(livro.Categoria);
                        categoriaRepository.AddCat(categoria);
                    }
                    
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
                }
            }
            contexto.SaveChanges();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
