namespace UtsDB.Domain.Data;

public enum Operation
{
    Read,
    Upsert,
    Insert,
    Update,
    Delete,
    DropTable,
    CreateTable
}