namespace Project_19._8.Services
{
	/// <summary>
	/// Предоставляет свойства сервиса коллекции элементов <see cref="IUrlButton"/>
	/// </summary>
	public interface IUrlButtonService 
	    : IUrlButtonService<IUrlButton, IUrlScript> { }

	/// <summary>
	/// Предоставляет свойства сервиса коллекции элементов <see cref="IUrlButton"/>
	/// </summary>
	/// <typeparam name="T1">Тип элемента коллекции <see cref="IUrlButton"/></typeparam>
	/// <typeparam name="T2">Тип используемых скриптов <see cref="IUrlScript"/></typeparam>
	public interface IUrlButtonService<T1, T2> 
	    where T1 : IUrlButton where T2 : IUrlScript
    {
	    /// <summary>
	    /// Элементы коллекции
	    /// </summary>
	    public IEnumerable<T1> Buttons { get; set; }
	    /// <summary>
	    /// Используемые скрипты
	    /// </summary>
	    public IEnumerable<T2> Scripts { get; set; }
    }
}
