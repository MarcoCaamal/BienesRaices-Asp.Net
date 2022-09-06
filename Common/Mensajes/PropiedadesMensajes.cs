namespace Common.Mensajes
{
    public static class PropiedadesMensajes
    {
        //Mensajes proceso correcto
        public static readonly string CreacionExitosa = "La propiedad se ha creado correctamente";
        public static readonly string EdicionExitosa = "La propiedad se ha actualizado correctamente";
        public static readonly string EliminacionExitosa = "La propiedad se ha eliminado correctamente";

        //Mensajes de procesos incorrectos
        public static readonly string CreacionError = "La propiedad no se ha podido crear, por favor revise sus datos y vuelva a intentarlo más tarde, si el problema persiste contacte a un administrador.";
        public static readonly string EdicionError = "La propiedad no se ha podido actualizar, por favor revise sus datos y vuelva a intentarlo más tarde, si el problema persiste contacte a un administrador.";
        public static readonly string EliminacionError = "La propiedad no se ha podido eliminar, por favor revise sus datos y vuelva a intentarlo más tarde, si el problema persiste contacte a un administrador.";

        //Mensajes de error
        public static readonly string ErrorImagen = "Ocurrio un error al intentar guardar la imagen, por favor revise sus datos y vuelva a intentarlo más tarde, si el problema persiste contacte a un administrador.";
        public static readonly string ErrorEliminarImagen = "Ocurrio un error al intentar eliminar la imagen, por favor revise sus datos y vuelva a intentarlo más tarde, si el problema persiste contacte a un administrador.";
    }
}
