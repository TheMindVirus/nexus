<?php
if (substr($_SERVER["QUERY_STRING"], 0, 8) == "https://")
{
    header("Content-Type: image/jpeg");
    echo "data:image/jpeg;base64," . base64_encode(file_get_contents($_SERVER["QUERY_STRING"]));
}
?>