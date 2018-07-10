recreate procedure download_file(s varchar(4000), n varchar(200))
returns (original_s varchar(4000), original_n varchar(4000),new_d varchar(4000))
external name 'FbNetFunctions!FbNetFunctions.Procedures.DownloadFile'
engine FbNetExternalEngine;

recreate procedure download_data(s varchar(4000))
returns (original_s varchar(4000), new_d BLOB SUB_TYPE 1)
external name 'FbNetFunctions!FbNetFunctions.Procedures.DownloadData'
engine FbNetExternalEngine;