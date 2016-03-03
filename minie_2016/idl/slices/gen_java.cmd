set ICE_HOME=C:\dev\ZeroC\Ice-3.6.1

%ICE_HOME%\bin\slice2java minie_entity.ice -I. -I%ICE_HOME%\slice --output-dir c:\vsdk_build\minie_java --underscore
%ICE_HOME%\bin\slice2java minie_service_base.ice -I. -I%ICE_HOME%\slice --output-dir c:\vsdk_build\minie_java --underscore
%ICE_HOME%\bin\slice2java minie_service_app.ice -I. -I%ICE_HOME%\slice --output-dir c:\vsdk_build\minie_java --underscore
@pause