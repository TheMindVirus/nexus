<?php if (substr($_SERVER["QUERY_STRING"], 0, 8) == "https://") { echo file_get_contents($_SERVER["QUERY_STRING"]); } ?>
