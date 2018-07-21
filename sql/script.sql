recreate procedure delete_file(f varchar(4000))
returns (original_f varchar(4000), b boolean)
external name 'FbNetFunctions!FbNetFunctions.Procedures.FileAppendLine'
engine FbNetExternalEngine;

recreate procedure file_append_line(f varchar(4000), l varchar(4000))
returns (original_f varchar(4000), original_l varchar(4000))
external name 'FbNetFunctions!FbNetFunctions.Procedures.FileAppendLine'
engine FbNetExternalEngine;

recreate procedure download_image(s varchar(4000), w int, h int, t int)
returns (i int, d varchar(4000))
external name 'FbNetFunctions!FbNetFunctions.Procedures.DownloadImage'
engine FbNetExternalEngine;

SET TERM ^ ;

CREATE OR ALTER procedure get_qr_code (
    numero varchar(50),
    codigo varchar(50))
as
declare variable temp blob sub_type 0 segment size 80;
declare variable part varchar(4000);
begin
    TEMP = '';
    for execute STATEMENT 'select d from DOWNLOAD_IMAGE(''https://selos.tjmg.jus.br/sisnor/eselo/consultaSeloseAtos.jsf?selo=' || NUMERO || '&codigo=' || CODIGO || ''', 150, 150, 500) order by i'
    into :PART
    do BEGIN
       TEMP = :TEMP || PART;
    END
    execute STATEMENT 'update SELOS set QR_CODE = x''' || TEMP || ''' where NUMERO = ''' || NUMERO || ''' and controle = ''' || CODIGO || '''';
    suspend;
end
^

SET TERM ; ^