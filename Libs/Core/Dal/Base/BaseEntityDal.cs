using System.ComponentModel.DataAnnotations;

namespace Core.Dal.Base;

/// <summary>
/// Базовая сущность для работы с сущностями в бд
/// </summary>
/// <typeparam name="T">тип идентификатор</typeparam>
public record BaseEntityDal<T>
{
    /// <summary>
    /// уникальный идентфиикатор сущности
    /// </summary>
    [Key]
    public T Id { get; init; }
}