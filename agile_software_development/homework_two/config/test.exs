import Config

# Configure your database
#
# The MIX_TEST_PARTITION environment variable can be used
# to provide built-in test partitioning in CI environment.
# Run `mix help test` for more information.
config :book_your_taxi, BookYourTaxi.Repo,
  username: "postgres",
  password: "postgres",
  database: "book_your_taxi_test#{System.get_env("MIX_TEST_PARTITION")}",
  hostname: "localhost",
  pool: Ecto.Adapters.SQL.Sandbox,
  pool_size: 10

# We don't run a server during test. If one is required,
# you can enable the server option below.
config :book_your_taxi, BookYourTaxiWeb.Endpoint,
  http: [ip: {127, 0, 0, 1}, port: 4001],
  secret_key_base: "mz7ZpRIQNuXRgkBxmNabPKr4T3fiWti75Xi6kHfxnSuy0JsG7Dmbsqjz/gZ8ko/H",
  server: true  # Change the `false` to `true`

# Add the following lines at the end of the file
config :hound, driver: "chrome_driver"
config :book_your_taxi, sql_sandbox: true

# In test we don't send emails.
config :book_your_taxi, BookYourTaxi.Mailer, adapter: Swoosh.Adapters.Test

# Print only warnings and errors during test
config :logger, level: :warn

# Initialize plugs at runtime for faster test compilation
config :phoenix, :plug_init_mode, :runtime
