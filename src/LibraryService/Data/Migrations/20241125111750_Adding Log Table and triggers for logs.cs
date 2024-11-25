using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryService.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingLogTableandtriggersforlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION public.logs_trbf (
                )
                RETURNS trigger LANGUAGE 'plpgsql'
                VOLATILE
                CALLED ON NULL INPUT
                SECURITY INVOKER
                PARALLEL UNSAFE
                COST 100
                AS
                $body$
                BEGIN
                    if new.mod_type <> 'DELETE' and 
                        coalesce(new.old_val,'') = coalesce(new.new_val,'') then
                        return null;
                    else
                        return new;
                    end if;
                END;
                $body$;

                ALTER FUNCTION public.logs_trbf ()
                OWNER TO postgres;
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE public.""Logs"" (
                    ts TIMESTAMP WITH TIME ZONE,
                    usr VARCHAR(10),
                    tbl VARCHAR(40),
                    fld VARCHAR(40),
                    pk_value VARCHAR,
                    mod_type CHAR(1),
                    old_val TEXT,
                    new_val TEXT
                );

                CREATE INDEX logs_idx1 ON public.""Logs""
                USING btree (ts);

                CREATE INDEX logs_idx2 ON public.""Logs""
                USING btree (tbl COLLATE pg_catalog.""default"", pk_value COLLATE pg_catalog.""default"");

                CREATE TRIGGER logs_trb
                BEFORE INSERT 
                ON public.""Logs""

                FOR EACH ROW 
                EXECUTE FUNCTION public.logs_trbf();

                ALTER TABLE public.""Logs""
                OWNER TO postgres;
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION public.log_function (
                )
                RETURNS trigger LANGUAGE 'plpgsql'
                VOLATILE
                CALLED ON NULL INPUT
                SECURITY INVOKER
                PARALLEL UNSAFE
                COST 100
                AS
                $body$
                DECLARE
                    fieldName varchar;
                    idValue TEXT;
                    oldValue TEXT;
                    newValue TEXT;
                    changeValue boolean = false;
                BEGIN
                    IF (TG_OP = 'INSERT') OR (TG_OP = 'UPDATE') THEN
                        -- NEW value
                        EXECUTE 'SELECT coalesce(($1).""Id""::text,'''')' INTO idValue USING NEW;

                        FOR fieldName IN
                            SELECT x.column_name
                            FROM information_schema.columns x
                            WHERE
                            x.table_schema = TG_TABLE_SCHEMA
                            AND x.table_name = TG_TABLE_NAME
                            and x.column_name <> 'Id'
                            ORDER BY ordinal_position
                        LOOP
                            -- NEW value
                            EXECUTE 'SELECT coalesce(($1).""' || fieldName || '""::text,'''')' INTO newValue USING NEW;
                            
                            -- OLD value
                            IF (TG_OP = 'INSERT') THEN
                                oldValue := ''::varchar;
                            ELSE   -- Else operation is an UPDATE, so capture the OLD value.
                                EXECUTE 'SELECT coalesce(($1).""' || fieldName || '""::text,'''')' INTO oldValue USING OLD;
                            END IF;

                            IF oldValue <> newValue THEN
                                changeValue = true;
                                insert into public.""Logs"" values(CURRENT_TIMESTAMP, substring(CURRENT_USER, 1, 10), 
                                    substring(TG_TABLE_NAME, 1, 40), substring(fieldName, 1, 40), 
                                    idValue, substring(tg_op, 1, 1), oldValue, newValue); 
                            END IF;
                        END LOOP;

                        RETURN NEW;
                    ELSEIF (TG_OP = 'DELETE') THEN
                        -- OLD value
                        EXECUTE 'SELECT coalesce(($1).""Id""::text,'''')' INTO idValue USING OLD;
                        FOR fieldName IN
                            SELECT x.column_name
                            FROM information_schema.columns x
                            WHERE
                            x.table_schema = TG_TABLE_SCHEMA
                            AND x.table_name = TG_TABLE_NAME
                            and x.column_name <> 'Id'
                            ORDER BY ordinal_position
                        LOOP
                            -- OLD value
                            EXECUTE 'SELECT coalesce(($1).""' || fieldName || '""::text,'''')' INTO oldValue USING OLD;

                            insert into public.""Logs"" values(CURRENT_TIMESTAMP, substring(CURRENT_USER, 1, 10), 
                                substring(TG_TABLE_NAME, 1, 40), substring(fieldName, 1, 40), 
                                idValue, substring(tg_op, 1, 1), oldValue, ''); 
                        END LOOP;

                        RETURN OLD;
                    END IF;
                END;
                $body$;

                ALTER FUNCTION public.log_function ()
                OWNER TO postgres;
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER log_books
                AFTER INSERT OR UPDATE OR DELETE 
                ON public.""Books""
                
                FOR EACH ROW 
                EXECUTE PROCEDURE public.log_function();
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER log_categories
                AFTER INSERT OR UPDATE OR DELETE 
                ON public.""Categories""
                
                FOR EACH ROW 
                EXECUTE PROCEDURE public.log_function();
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER log_images
                AFTER INSERT OR UPDATE OR DELETE 
                ON public.""Images""
                
                FOR EACH ROW 
                EXECUTE PROCEDURE public.log_function();
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER log_loans
                AFTER INSERT OR UPDATE OR DELETE 
                ON public.""Loans""
                
                FOR EACH ROW 
                EXECUTE PROCEDURE public.log_function();
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER log_reservations
                AFTER INSERT OR UPDATE OR DELETE 
                ON public.""Reservations""
                
                FOR EACH ROW 
                EXECUTE PROCEDURE public.log_function();
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER log_users
                AFTER INSERT OR UPDATE OR DELETE 
                ON public.""Users""
                
                FOR EACH ROW 
                EXECUTE PROCEDURE public.log_function();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TABLE IF EXISTS public.""Logs"" CASCADE;");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.log_function CASCADE;");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS log_books ON public.""Books"";");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS log_categories ON public.""Categories"";");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS log_images ON public.""Images"";");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS log_loans ON public.""Loans"";");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS log_reservations ON public.""Reservations"";");
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS log_users ON public.""Users"";");
        }
    }
}
