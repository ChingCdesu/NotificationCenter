namespace NotificationCenter.Enums;

public enum RolePermission : ulong
{
    None = 0,
    
    ReadRole = 1,
    WriteRole = 2,
    ReadUser = 4,
    WriteUser = 8,
    
    ReadDevice = 16,
    WriteDevice = 32,
    ReadService = 64,
    WriteService = 128,
    ReadServiceAccount = 256,
    WriteServiceAccount = 512,
    ReadEndpoint = 1024,
    WriteEndpoint = 2048,
    ReadTemplate = 4096,
    WriteTemplate = 8192,
    
    All = ulong.MaxValue
}