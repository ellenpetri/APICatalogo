using ApiCatalogo.Context;
using ApiCatalogo.Interface;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repositories;

public class CategoriaRepository(AppDbContext context) : Repository<Categoria>(context), ICategoriaRepository
{
    public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParams)
    {
        IQueryable<Categoria> categorias = GetAll().OrderBy(p => p.CategoriaId).AsQueryable();

        PagedList<Categoria> categoriasOrdenados = PagedList<Categoria>.ToPagedList(categorias, categoriasParams.PageNumber, categoriasParams.PageSize);

        return categoriasOrdenados;
    }

    public PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasParams)
    {
        IQueryable<Categoria> categorias = GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(categoriasParams.Nome))
            categorias = categorias.Where(c => c.Nome.Contains(categoriasParams.Nome));

        PagedList<Categoria> categoriasFiltradas = PagedList<Categoria>.ToPagedList(categorias, categoriasParams.PageNumber, categoriasParams.PageSize);

        return categoriasFiltradas;
    }
}