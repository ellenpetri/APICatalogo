using ApiCatalogo.Context;
using ApiCatalogo.Interface;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repositories;

public class ProdutoRepository(AppDbContext context) : Repository<Produto>(context), IProdutoRepository
{
    public PagedList<Produto> GetProdutos(ProdutosParameters produtosParams)
    {
        IQueryable<Produto> produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();

        PagedList<Produto> produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtosParams.PageNumber, produtosParams.PageSize);

        return produtosOrdenados;
    }
    public PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParams)
    {
        IQueryable<Produto> produtos = GetAll().AsQueryable();

        if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
        {
            if (produtosFiltroParams.PrecoCriterio.ToLower().Equals("maior"))
                produtos = produtos.Where(p => p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            else if (produtosFiltroParams.PrecoCriterio.ToLower().Equals("menor"))
                produtos = produtos.Where(p => p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            else if (produtosFiltroParams.PrecoCriterio.ToLower().Equals("igual"))
                produtos = produtos.Where(p => p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
        }

        PagedList<Produto> produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);

        return produtosFiltrados;
    }
    public IEnumerable<Produto> GetProdutoByCategoria(int id)
    {
        return GetAll().Where(c => c.CategoriaId == id);
    }

}