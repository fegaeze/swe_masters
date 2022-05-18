import Config

# Configure your database
#
# The MIX_TEST_PARTITION environment variable can be used
# to provide built-in test partitioning in CI environment.
# Run `mix help test` for more information.
config :exam, Exam.Repo,
  username: "postgres",
  password: "postgres",
  database: "exam_test#{System.get_env("MIX_TEST_PARTITION")}",
  hostname: "localhost",
  pool: Ecto.Adapters.SQL.Sandbox,
  pool_size: 10

# We don't run a server during test. If one is required,
# you can enable the server option below.
config :exam, ExamWeb.Endpoint,
  http: [ip: {127, 0, 0, 1}, port: 4002],
  secret_key_base: "i/auizRcVEakhbP2mVlGlcsR2BZSkd0BfII1MQGNXj8XvQcba6k86ExXrBuW1SUL",
  server: true

# In test we don't send emails.
config :exam, Exam.Mailer, adapter: Swoosh.Adapters.Test

# Print only warnings and errors during test
config :logger, level: :warn

# Initialize plugs at runtime for faster test compilation
config :phoenix, :plug_init_mode, :runtime

config :hound, driver: "chrome_driver"
config :exam, sql_sandbox: true
