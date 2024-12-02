using System.Reflection;

namespace Gyneco.Application.Extensions;

public static class EntityExtensions
{
    public static TDTO ToDTO<TEntity, TDTO>(this TEntity entity)
        where TEntity : class
        where TDTO : class
    {
        try
        {
            if (entity == null) throw new Exception(nameof(TEntity));

            TDTO dto = Activator.CreateInstance<TDTO>();

            //Set value if the entity has a property with the SAME NAME and that property is NOT NULL
            foreach (PropertyInfo dtoPropertyInfo in typeof(TDTO).GetProperties().Where(x => typeof(TEntity).GetProperty(x.Name) != null))
            {
                var entityPropertyInfo = typeof(TEntity).GetProperty(dtoPropertyInfo.Name);
                dtoPropertyInfo.SetValue(dto, entityPropertyInfo.GetValue(entity));
            }

            return dto;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    
    public static TEntity ToNewEntity<TDTO, TEntity>(this TDTO dto)
        where TEntity : class
        where TDTO : class
    {
        TEntity entity;
        try
        {
            if (dto == null) throw new Exception(nameof(TEntity));

            entity = Activator.CreateInstance<TEntity>();

            foreach (PropertyInfo dtoProperty in (typeof(TDTO)).GetProperties())
            {
                if (typeof(TEntity).GetProperty(dtoProperty.Name) != null && dtoProperty.GetValue(dto) != null)
                {
                    PropertyInfo entityProperty = typeof(TEntity).GetProperty(dtoProperty.Name);
                    entityProperty.SetValue(entity, dtoProperty.GetValue(dto));
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        return entity;
    }
}