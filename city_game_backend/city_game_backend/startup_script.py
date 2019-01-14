from player_manager.models import ActivePlayer
import logging
import psycopg2

logger = logging.getLogger(__name__)


# This function is only run once on the server startup
def on_startup():
    logger.info("Running the startup script..")

    try:
        ActivePlayer.objects.all().delete()
        logger.info("Startup script finished successfully")
    except:
        logger.warning("""A problem has occurred during the startup script,
don't worry if you're running it for the first time""")




