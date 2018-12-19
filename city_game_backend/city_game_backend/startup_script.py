from player_manager.models import ActivePlayer
import logging

logger = logging.getLogger(__name__)


# This function is only run once on the server startup
def on_startup():
    logger.info("Running the startup script..")

    ActivePlayer.objects.all().delete()

    logger.info("Startup script finished successfully")

