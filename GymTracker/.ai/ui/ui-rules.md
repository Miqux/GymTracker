# UI Guidelines for Gym Tracker

These guidelines define the overall UI principles and standards for our Gym Tracker application. All front-end work should align with these rules to ensure a modern, professional, and cohesive user experience.

---

## 1. Frameworks and Packages

- **Bootstrap 5**
  - Use Bootstrap 5 as the core CSS framework to provide consistency in layout, responsiveness, and pre-built components.
- **Bootstrap Icons**
  - Leverage Bootstrap Icons for all iconography to maintain modern and unified visuals.
- **Sass**
  - Use Sass for managing custom themes, variables (colors, fonts, spacings), and to override Bootstrap defaults as needed.

---

## 2. Styling

### Color Palette and Typography

- **Centralized Color Palette**
  - **Primary Colors**: Use a professional gray palette to ensure consistency.
    - **Charcoal**: `#343a40` – Ideal for headings and primary text.
    - **Slate**: `#495057` – Suitable for secondary text and icons.
    - **Silver**: `#adb5bd` – For borders, dividers, and background accents.
    - **Light Gray**: `#f8f9fa` – As the main background color.
    - **Accent**: Use a subtle blue (e.g., `#0d6efd`) for call-to-action elements to draw attention.
  - **Sass Variables Example**:
$color-primary: #343a40;
$color-secondary: #495057;
$color-border: #adb5bd;
$color-background: #f8f9fa;
$color-accent: #0d6efd;

- **Typography**
  - Base typography should use Bootstrap variables or custom Sass files.
  - Use a sans-serif font stack with consistent font sizes, weights, and line-heights across the application.
  - Example: Use `$font-family-base: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;`  

### Spacing and Layout

- Utilize Bootstrap's spacing utilities (e.g., `.mb-3`, `.mt-3`, `.p-2`) for consistent margins and paddings.
- Maintain ample whitespace and a consistent grid structure to create a clean and professional layout.

### Form Validation

- Display validation feedback using Bootstrap’s `.invalid-feedback` and `.valid-feedback` classes.
- Ensure error messages are easily visible, using colors that complement the overall gray palette.

---

## 3. UI Components

### Forms

- Use semantic HTML with `<form>` tags, ensuring that each control has a corresponding `<label>`.
- Apply Bootstrap classes such as `.form-control`, `.form-select`, and `.form-check` to ensure consistency.
- Utilize the gray tones for borders and backgrounds to maintain color consistency.

### Buttons

- Standardize button usage with Bootstrap classes (e.g., `.btn`, `.btn-primary`, `.btn-secondary`).
- **Primary Actions**: Use the accent color (e.g., `#0d6efd`) for primary buttons with light text.
- **Secondary Actions**: Use one of the gray tones (e.g., dark gray or slate) for secondary buttons to keep the visual harmony.

### Tables and Lists

- For tabular data, use Bootstrap tables with classes like `.table`, `.table-striped`, and `.table-responsive`.
- Adjust table styles to align with the gray palette:
  - Table headers: Use a darker gray background with light text.
  - Dividers and borders: Use the silver (`#adb5bd`) tone.
- For summary views, consider using Bootstrap’s card components styled with the gray color scheme.

### Modals and Dialogs

- Use Bootstrap modals for alerts and confirmations.
- Ensure that modal backgrounds, borders, and text adhere to the defined color palette for a cohesive look.

### Navigation

- Utilize Bootstrap’s navbar and breadcrumb components for clear and consistent navigation.
- Adjust navbar backgrounds and link styles to match the gray-themed palette, ensuring active states are clearly indicated.

---

## 4. Layout and Responsiveness

### Grid System

- Rely on Bootstrap's grid system (`.container`, `.row`, `.col-*`) to build responsive layouts.
- Use consistent header, content, and footer layouts to maintain a streamlined interface across pages.

### Consistent Layout Patterns

- Standardize page layouts with common headers, content areas, and footers.
- Develop reusable partial views for common UI regions, such as form groups, alerts, and navigation bars, ensuring uniform styling and easier maintenance.

---

## 5. Best Practices

### Semantic HTML and Accessibility

- Always use semantic HTML elements (e.g., `<header>`, `<main>`, `<footer>`) where applicable.
- Incorporate ARIA roles and labels to enhance accessibility for all users.

### Reusable UI Components

- Create common partial views (e.g., for form groups, alerts, and modals) and reuse them across pages.
- Document any deviations or customizations clearly to maintain consistency.

### Error Handling and Alerts

- Standardize error messages and alerts with a uniform appearance:
  - Use the defined palette for backgrounds, borders, and text.
  - Apply Bootstrap’s alert classes (e.g., `.alert`, `.alert-danger`, `.alert-success`) combined with custom gray settings if necessary.

### Documentation

- Keep this guideline up-to-date as UI requirements evolve.
- Clearly document any customizations in the codebase and supplementary documentation.

---

Following these guidelines will ensure that the Gym Tracker application 