using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.LogicalEntities.SEG;
using System;
using System.Collections.Generic;

namespace SYS_SGI.Infrastructure.Data.Implementation.IRepositories.SEG
{
    public interface IUsuariosRepository : IDisposable
    {
        UsuariosLogic Login(string usuario, string contrasena);

        SistemaLogic Sistema_x_Token(string token, int codigo_usuario);

        List<OpcionLogic> Usuario_x_Sistema(string usuario, int codigo_sistema);

        UsuariosLogic Obtener(float codigo);
        List<UsuariosLogic> Listar();
        List<UsuariosLogic> Paginacion(PaginateParams paginateParams);
        AppResponse Mantenimiento(List<UsuariosLogic> lista, string accion);
    }
}
