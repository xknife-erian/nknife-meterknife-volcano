using RAY.Common.Communication;
using RAY.Common.Provider;

namespace NKnife.Circe.Base.Modules.Data
{
    public interface IInstrumentDataProvider : IFileDataProvider
    {
        /*
        #region Workstation Interfaces
        IProviderResult AddWorkstation(IWorkstation entity);

        IProviderResult DeleteWorkstation(string uniqueId);

        IProviderResult UpdateWorkstation(IWorkstation entity);
        
        IWorkstation? GetWorkstationByName(string name, bool transparent = false);

        Task<IWorkstation?> GetWorkstationByNameAsync(string name, bool transparent = false);

        IWorkstation? GetWorkstation(string uniqueId, bool transparent = false);
        Task<IWorkstation?> GetWorkstationAsync(string uniqueId, bool transparent = false);

        List<IWorkstation> GetAllWorkstations(bool transparent = false);
        Task<List<IWorkstation>> GetAllWorkstationsAsync(bool transparent = false);

        bool IsWorkstationNameAvailable(string name, string? exceptUniqueId = null);
        #endregion

        #region Elements Interfaces
        #region Add
        /// <summary>
        ///     添加一个新的电机
        /// </summary>
        /// <typeparam name="TMotor"></typeparam>
        /// <param name="wsUniqueId"></param>
        /// <param name="parentUniqueId"></param>
        /// <param name="motor"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DataIsNotFoundForUpdate"></exception>
        /// <exception cref="DataWithTheIdAlreadyExists"></exception>
        IProviderResult AddMotor<TMotor>(string wsUniqueId, string parentUniqueId, TMotor motor)
            where TMotor : IMotor;

        /// <summary>
        ///     添加一个新的组件（包括移液臂, FrontArm, Gripper等）
        /// </summary>
        /// <typeparam name="TSubassembly"></typeparam>
        /// <param name="wsUniqueId"></param>
        /// <param name="parentUniqueId"></param>
        /// <param name="subassembly"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DataIsNotFoundForUpdate"></exception>
        /// <exception cref="DataWithTheIdAlreadyExists"></exception>
        IProviderResult AddSubassembly<TSubassembly>(string wsUniqueId, string parentUniqueId, TSubassembly subassembly)
            where TSubassembly : IArmSubassembly;

        /// <summary>
        ///     添加一个新的布局
        /// </summary>
        /// <typeparam name="TBluePrint"></typeparam>
        /// <param name="wsUniqueId"></param>
        /// <param name="blueprint"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DataIsNotFoundForUpdate"></exception>
        /// <exception cref="DataWithTheIdAlreadyExists"></exception>
        IProviderResult AddBluePrint<TBluePrint>(string wsUniqueId, TBluePrint blueprint)
            where TBluePrint : IBluePrint;

        /// <summary>
        ///     添加一个通信连接
        /// </summary>
        /// <typeparam name="TComlink"></typeparam>
        /// <param name="wsUniqueId"></param>
        /// <param name="comlink"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DataIsNotFoundForUpdate"></exception>
        /// <exception cref="DataWithTheIdAlreadyExists"></exception>
        IProviderResult AddComLink<TComlink>(string wsUniqueId, TComlink comlink)
            where TComlink : ICommlink;
        #endregion

        #region Delete
        /// <summary>
        ///     删除一个电机
        /// </summary>
        /// <param name="wsUniqueId"></param>
        /// <param name="motorUniqueId"></param>
        /// <returns></returns>
        /// <exception cref="DataIsNotFoundForUpdate"></exception>
        bool DeleteMotor(string wsUniqueId, string motorUniqueId);

        /// <summary>
        ///     删除一个组件(移液臂, FrontArm, Gripper等)
        /// </summary>
        /// <param name="wsUniqueId"></param>
        /// <param name="assemblyUniqueId"></param>
        /// <returns></returns>
        /// <exception cref="DataIsNotFoundForUpdate"></exception>
        bool DeleteSubassembly(string wsUniqueId, string assemblyUniqueId);

        /// <summary>
        ///     删除一个设计图纸
        /// </summary>
        /// <param name="wsUniqueId"></param>
        /// <param name="bpVarietyId"></param>
        /// <returns></returns>
        /// <exception cref="DataIsNotFoundForUpdate"></exception>
        bool DeleteBluePrint(string wsUniqueId, string bpVarietyId);

        /// <summary>
        ///     删除一个设计通信连接
        /// </summary>
        /// <param name="wsUniqueId"></param>
        /// <param name="comlinkVarietyId"></param>
        /// <returns></returns>
        /// <exception cref="DataIsNotFoundForUpdate"></exception>
        bool DeleteComLink(string wsUniqueId, string comlinkVarietyId);
        #endregion

        #region Update
        IProviderResult UpdateMotor<TMotor>(string wsUniqueId, TMotor entity)
            where TMotor : IMotor;

        /// <summary>
        ///     更新指定地址的电机的控制参数集合
        /// </summary>
        /// <param name="wsSerialNumber">工作站编号</param>
        /// <param name="armUniqueId"></param>
        /// <param name="address">指定电机的地址</param>
        /// <param name="parameters">控制参数集合</param>
        IProviderResult UpdateMotorParameters(string wsSerialNumber,
                                              string armUniqueId,
                                              byte address,
                                              IDictionary<byte, uint> parameters);

        IProviderResult UpdateSubassembly<TSubassembly>(string wsUniqueId, TSubassembly entity)
            where TSubassembly : IArmSubassembly;

        IProviderResult UpdateBluePrint<TBluePrint>(string wsUniqueId, TBluePrint entity)
            where TBluePrint : IBluePrint;

        IProviderResult UpdateComLink<TComlink>(string wsUniqueId, TComlink entity)
            where TComlink : ICommlink;

        IProviderResult UpdateComLinkList(string wsUniqueId, List<ICommlink> entities);
        #endregion

        #region Get
        List<string> GetAllComLinkRemote();
        List<string> GetAllComLinkRemoteWithinWorkstation(string wsCode);
        List<byte> GetAllMotorAddress();
        List<byte> GetAllMotorAddressWithinWorkstation(string wsCode);
        List<IMotor> GetAllMotors();
        List<IMotor> GetAllMotorsWithinWorkstation(string wsCode);
        #endregion

        bool IsComLinkAvailable(ICommlink entity, string? expectRemote = null);

        bool IsMotorAddressAvailable(IMotor entity);

        bool IsComLinkAvailableWithinWorkstation(string wsCode, ICommlink entity, string? expectRemote = null);

        bool IsMotorAddressAvailableWithinWorkstation(string wsCode, IMotor entity);
        #endregion

        IProviderResult AddExtensionDevice(string wsUniqueId, ExtensionDevice entity);
        IProviderResult DeleteExtensionDevice(string wsUniqueId, string driverUniqueId);
        IProviderResult UpdateExtensionDevice(string wsUniqueId, ExtensionDevice entity);
        bool IsExtensionDeviceAliasAvailable(string wsUniqueId, ExtensionDevice entity);

        IProviderResult AddFunctionBoard(string wsUniqueId, IFunctionBoard entity);
        IProviderResult DeleteFunctionBoard(string wsUniqueId, string boardId);
        IProviderResult UpdateFunctionBoard(string wsUniqueId, IFunctionBoard entity);
        bool IsFunctionBoardAliasAvailable(string wsUniqueId, IFunctionBoard entity);
        */

    }
}