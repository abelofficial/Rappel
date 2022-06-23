import { CreateTodoCommand } from "../../types";
import { string, SchemaOf, object } from "yup";

export const fieldLabels: CreateTodoCommand = {
  title: "Title",
  description: "Description",
};

export const fieldNames: CreateTodoCommand = {
  title: "title",
  description: "description",
};

export const initialValues: CreateTodoCommand = {
  title: "",
  description: "",
};

export const validationSchema: SchemaOf<CreateTodoCommand> = object({
  title: string().required("Title is required"),
  description: string()
    .required("Description is required")
    .min(20, "Description must be min 20 characters."),
});
