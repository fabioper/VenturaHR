module.exports = {
  content: [
    "./src/pages/**/*.{js,ts,jsx,tsx}",
    "./src/components/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    container: {
      center: true,
      padding: "1rem",
    },
    fontFamily: {
      display: ["Jost"],
      body: ["Inter"],
    },
    extend: {
      boxShadow: {
        behind: "0 -2px 45px -14px var(--tw-shadow-color)",
      },
    },
  },
  plugins: [],
  corePlugins: {
    preflight: false,
    divideStyle: true,
  },
}
