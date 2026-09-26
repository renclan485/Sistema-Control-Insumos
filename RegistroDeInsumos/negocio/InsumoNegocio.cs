using System;
using System.Collections.Generic;
// Asegúrate de que este using coincida con el namespace de tu capa de datos
using SistemaProductosApp.Datos; 

namespace RegistroDeInsumos.negocio
{
    public class InsumoNegocio
    {
        // Instancia de la capa de datos
        private readonly Insumos _insumosDatos = new Insumos();

        // 1. Crear Insumo con validaciones de negocio
        public bool Crear(Insumo insumo)
        {
            // Reglas de negocio (Validaciones previas a la base de datos)
            if (string.IsNullOrWhiteSpace(insumo.Nombre))
            {
                throw new ArgumentException("El nombre del insumo no puede estar vacío.");
            }
            if (insumo.Stock < 0)
            {
                throw new ArgumentException("El stock inicial no puede ser negativo.");
            }
            if (insumo.IdTipoInsumo <= 0)
            {
                throw new ArgumentException("Debe seleccionar un tipo de insumo válido.");
            }

            // Llamada a la capa de datos
            return _insumosDatos.CrearInsumo(insumo);
        }

        // 2. Leer Insumos (Todos o por ID)
        public List<Insumo> ObtenerTodos()
        {
            // Pasa null para traer la lista completa de activos
            return _insumosDatos.LeerInsumos(null);
        }

        public Insumo ObtenerPorId(int idInsumo)
        {
            if (idInsumo <= 0)
            {
                throw new ArgumentException("El ID del insumo debe ser mayor a cero.");
            }

            List<Insumo> resultado = _insumosDatos.LeerInsumos(idInsumo);
            
            // Retorna el primer insumo encontrado o null si no existe
            if (resultado.Count > 0)
            {
                return resultado[0];
            }
            return null;
        }

        // 3. Actualizar Insumo
        public bool Actualizar(Insumo insumo)
        {
            if (insumo.IdInsumo <= 0)
            {
                throw new ArgumentException("El ID del insumo a actualizar no es válido.");
            }
            if (string.IsNullOrWhiteSpace(insumo.Nombre))
            {
                throw new ArgumentException("El nombre del insumo no puede estar vacío.");
            }
            if (insumo.Stock < 0)
            {
                throw new ArgumentException("El stock no puede ser negativo.");
            }

            return _insumosDatos.ActualizarInsumo(insumo);
        }

        // 4. Eliminar Lógicamente un Insumo
        public bool Eliminar(int idInsumo)
        {
            if (idInsumo <= 0)
            {
                throw new ArgumentException("El ID del insumo a eliminar no es válido.");
            }

            return _insumosDatos.EliminarInsumo(idInsumo);
        }
    }
}