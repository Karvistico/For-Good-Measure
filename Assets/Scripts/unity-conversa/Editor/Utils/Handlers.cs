using UnityEditor;

namespace Conversa.Editor
{
    public static class Handlers
    {
       
        private static TypeCache.MethodCollection GetEditPropertyHandlers() =>
            TypeCache.GetMethodsWithAttribute<EditPropertyHandlerAttribute>();

        private static TypeCache.MethodCollection GetDeletePropertyHandlers() =>
            TypeCache.GetMethodsWithAttribute<DeletePropertyHandlerAttribute>();

        private static TypeCache.MethodCollection GetDeleteSelectionHandlers() =>
            TypeCache.GetMethodsWithAttribute<DeleteSelectionHandlerAttribute>();

        public static void ExecuteEditProperty(object[] args)
        {
            foreach (var handler in GetEditPropertyHandlers())
                handler.Invoke(null, args);
        }

        public static void ExecuteDeleteProperty(object[] args)
        {
            foreach (var handler in GetDeletePropertyHandlers())
                handler.Invoke(null, args);
        }

        public static void ExecuteDeleteNodes(object[] args)
        {
            foreach (var handler in GetDeleteSelectionHandlers())
                handler.Invoke(null, args);
        }
    }
}